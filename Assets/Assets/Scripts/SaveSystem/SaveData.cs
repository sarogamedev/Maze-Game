namespace Assets.Scripts
{
    [System.Serializable]
    public class SaveData
    {
        public int level;

        public SaveData (GameManager gameManager)
        {
            level = gameManager.levelCount;
        }
    }
}
