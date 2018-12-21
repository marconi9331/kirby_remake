using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirbyGame
{
    public class GameConstants
    {
        //tags
        public const string PLAYER_TAG = "player";
        public const string BORDER_TAG = "border";
        public const string GROUND_TAG = "ground";
        public const string FINISH_TAG = "Finish";
        public const string HAZARD_TAG = "Hazards";
        public const string SAND_TAG = "Sand";
        public const string WALL_TAG = "wall";
        public const string ENEMY_TAG = "enemy";
        public const string ENEMY_BULLET_TAG = "projectile";
        public const string DOOR_TAG = "door";
        public const string COLLECTIBLE_TAG = "collectible";

        //Layer Maks
        public const int PLAYER_LAYER_MASK = 8;

        //Speed Values
        public const float PLAYER_SPEED_MULTIPLIER = 0.5F;
        public const float PLAYER_SPEED_MAX = 35f;
        public const float CANNON_BALL_FORCE = 100f;
        public const float FLYING_SAUCER_SPEED = 7f;

        //timer values
        public const float CANNON_BALL_RELOAD = 3F;
        public const float SQUID_CHARGE_TIME = 3F;
        public const float SQUID_RESET_TIMER = 4F;

        //Inventory
        public const int PLAYER_HEALTH = 3;
        public const int CANNON_BALL_NUMBER = 10;
    }
}