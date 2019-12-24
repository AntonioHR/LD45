
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace SamuraiGame.Sword {
    public class SwordFlyingModel {
        [SerializeField]
        private SpriteAtlas swordFlyingSpritesheet;

        private Dictionary<string, Sprite> spriteSheetDictionary;

        public Dictionary<string, Sprite> swordSpritesheet { get {
                if(spriteSheetDictionary == null)
                {
                    CreateDictionary();
                }
                return spriteSheetDictionary;
            }
        }

        private void CreateDictionary()
        {
            Sprite[] sprites = new Sprite[swordFlyingSpritesheet.spriteCount];
            swordFlyingSpritesheet.GetSprites(sprites);

            spriteSheetDictionary = new Dictionary<string, Sprite>();
            foreach (Sprite sprite in sprites)
            {
                spriteSheetDictionary.Add(sprite.name, sprite);
            }
        }

    }
}