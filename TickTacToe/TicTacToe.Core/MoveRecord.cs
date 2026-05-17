using System;

namespace TicTacToe.Core;

// Immutable type megvalósítása (követelmény: saját immutable type / record class)
public record class MoveRecord(int X, int Y, string Player);