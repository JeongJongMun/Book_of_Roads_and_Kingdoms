public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Enemy,
        Weapon
    }

    public enum Skills
    {
        Born        = 1,
        Candle      = 2,
        Koran       = 3,
        Wand        = 4,
        Cat         = 5,
        Camel       = 6,
        Shortbow    = 7,
        Damascus    = 8,
        Samshir     = 9,
        Water       = 10,
        Gold        = 11,
        Shield      = 12,
        Armor       = 13,

    }
    public enum MonsterStyle
    {
        unknown = 0,
        zombie = 1,
        zombieElite = 2,
        skeleton = 3,
        skeletonElite = 4,
        tombStone = 5
    }
    public enum MonsterType
    {
        unknown,
        Enemy,
        middleBoss,
        Boss
    }
    public enum PlayerStartWeapon
    {
        Lightning = 1,
        Shotgun = 2
    }

    public enum PopupUIGroup
    {
        Unknown,
        UI_GameMenu,
        UI_ItemBoxOpen,
        UI_LevelUp,
        UI_GameOver,
        UI_GameVictory,
        UI_TimeStop,
        UI_CharacterSelect
    }

    public enum SceneUI
    {
        Unknown,
        UI_Player,
        UI_MainMenu,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum BGMs
    {
        BGM_01,
        BGM_02,
        BGM_03,
        BGM_04,
        A_Bit_Of_Hope
    }
    public enum UIEvent
    {
        Click,
        Drag,

    }
    public enum SceneType
    {
        Unknown,
        GameScene,
        MainMenuScene
    }

}
