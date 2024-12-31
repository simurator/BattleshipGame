﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal abstract class GameState
    {
        public abstract void NextState(Game game);
        public abstract void Handle(Game game);
    }
}