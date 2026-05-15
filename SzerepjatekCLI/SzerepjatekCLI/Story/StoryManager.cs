using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

using SzerepjatekCLI.Core;
using SzerepjatekCLI.Items;
using SzerepjatekCLI.Services.JsonLoaders;
using SzerepjatekCLI.Utils;

namespace SzerepjatekCLI.Story
{
    internal class StoryManager
    {
        private Dictionary<string, StoryNode> _nodes;
        private GameState _state;

        public StoryManager()
        {
            LoadStory();
        }

        private void LoadStory()
        {
            try
            {
                //AppContext.BaseDirectory
                var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Data", "story.json"));
                using var doc = JsonDocument.Parse(json);
                var storyArray = doc.RootElement.GetProperty("story").GetRawText();
                var list = JsonSerializer.Deserialize<List<StoryNode>>(storyArray);
                _nodes = list
                    .Where(n => !string.IsNullOrWhiteSpace(n.Id))
                    .ToDictionary(n => n.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading story: {ex.Message}");
            }
        }

        public StoryNode GetNode(string id)
        {
            if (_nodes.TryGetValue(id, out var node))
            {
                return node;
            }
            throw new Exception("Story node not found");
        }

        public bool IsEndNode(string id)
        {
            var node = GetNode(id);
            return node.Choices == null || node.Choices.Count == 0;
        }
        public GameState? HandleShopAction(string action, GameState state = null) //lehet hogy private lesz, mert csak a StoryManager-en belül használjuk
        {
            GameState boughtState;
            if(state == null)
            {
                return null;
            }
            switch (action)
            {
                case "shop_blacksmith":
                    boughtState = OpenBlacksmithShop(state);
                    if(boughtState == null)
                    {
                        return state;
                    }
                    return boughtState;
                /*case "shop_market":
                    return OpenMarketShop();*/
                case "shop_alchemist":
                    boughtState =  OpenAlchemistShop(state);
                    if (boughtState == null)
                    {
                        return state;
                    }
                    return boughtState;
            }
            return state;
        }
        private GameState? OpenBlacksmithShop(GameState state = null)
        {
            Console.WriteLine("Kovács bolt megnyitva");
            WeaponLoadService _weaponService = new WeaponLoadService();
            for (int i = 1; i < _weaponService.Weapons.Count; i++)
            {
                Console.WriteLine($"{i}. Fegyver neve: {_weaponService.Weapons[i].Name} , Sebzés: {_weaponService.Weapons[i].Damage} Ár: {_weaponService.Weapons[i].Weight} ezüst");
            }
            Console.WriteLine($"Nyomj {_weaponService.Weapons.Count + 1} ha nem akarosz semmit se venni.");
            int choise = InputResult.ReadPureIntInRange("Válassz egy fegyvert (a sorszám alapján): ", 1, _weaponService.Weapons.Count + 1);
            if (choise == _weaponService.Weapons.Count + 1)
            {
                return null;
            }
            if (!IsItemAffordable(_weaponService.GetWeaponById(choise), state, 1))
            {
                return null;
            }
            state.Player.Inventory.Add(_weaponService.GetWeaponById(choise));
            state.Player.Inventory.RemoveMoney(Money.Ezust, _weaponService.GetWeaponById(choise).Weight);

            return state;
        }

       /* private GameState? OpenMarketShop(GameState state)
        {
            Console.WriteLine("Piac megnyitva");
            

        }*/
        private GameState? OpenAlchemistShop(GameState state)
        {
            Console.WriteLine("Alkimista bolt megnyitva");
            PotionLoadService _potionService = new PotionLoadService();
            for (int i = 1; i < _potionService.Potions.Count; i++)
            {
                Console.WriteLine($"{i}. {_potionService.Potions[i].ToString} Ár: {_potionService.Potions[i].Weight * 10} ezüst");
            }
            Console.WriteLine($"Nyomj {_potionService.Potions.Count} ha nem akarosz semmit se venni.");
            int choise = InputResult.ReadPureIntInRange("Válassz egy varázsitalt (a sorszám alapján): ", 1, _potionService.Potions.Count + 1);
            if (choise == _potionService.Potions.Count)
            {
                return null;
            }
            if (!IsItemAffordable(_potionService.GetPotionById(choise), state, 10))
            {
                return null;
            }
            state.Player.Inventory.Add(_potionService.GetPotionById(choise));
            state.Player.Inventory.RemoveMoney(Money.Ezust, _potionService.GetPotionById(choise).Weight * 10);

            return state;
        }


        private bool IsItemAffordable(Item item, GameState state, int arSzorzo)
        {
            if (item == null || state == null) return false;

            if(!state.Player.Inventory.HasEnoughMoney(Money.Ezust, item.Weight * arSzorzo))
            {
                Console.WriteLine($"Sajnálom, de nincs elég pénzed megvenni ezt a tárgyat. Ennyibe kerül a tárgy: {item.Weight * arSzorzo} ezüst");
                return false;
            }
           
            if (state.Player.Inventory.CurrentWeight + item.Weight > state.Player.Inventory.MaxWeight)
            {
                Console.WriteLine($"Sajnálom, de nem fér el a hátizsákodban ez a tárgy. Amennyid most van: {state.Player.Inventory.CurrentWeight}, A tárgy súlya: {item.Weight}");
                return false;
            }
            return true;
        }

        private void Fight()
        {
            //harcrendszer
        }
    }
}
//kapok egy végtelen loopot ha bemegyek egy boltba, a story.jsonben be van égetve egy vásárlás azt ki kell törölni, 
//A GameLoopban a vásárlást át kell nézni, amiatt van a végtelen loop mert nem lépek tovább
//Frucsa  a bolt kiírás a fizetés sem működik
//json serializcióban van olyan hogy le tudom tárolni a weapon és money elemeket külön nem csak itemeket
//ha van ey leszármazási fám és az ősosztályt szerializálom akkor hogyan lehet hogy a leszármazott ősosztályok szerializálódjanak
//Az id lehetne ékezetes magyarul és mindenho lmint cím/helyszín ki lehetne iratni, és nem kell egy címet mindegyikhez hozzáadni a sztoriban
//kilépésnél hiba van mert másra is ment kilépésnél nem csak a menüre
//Az inventory is lehetne class amin belül van egy list<item> és lehet annak is toStringje amiben lehet orderby order ami az item egy fieldje, szebb lesz tőle kiírtaáskor, lehet egy add, egy list metódus, könnyebb lekérni a moneyt, bele mehet abba a classba az ofType