using Assets.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tools
{
    public static class GameobjectExtensions
    {
        public static void LoadScriptableObject<T>(this GameObject GO, T ScriptableObject)
        {
            GO.GetComponent<IScriptableObjectRenderer<T>>().Init(ScriptableObject);
        }

        public static void CenterSpriteOnPivotY(this Image img)
        {
            float offsetY = img.rectTransform.rect.height * Mathf.Abs((img.sprite.pivot.y / img.sprite.rect.height) - .5f);

            img.rectTransform.offsetMin = new Vector2(img.rectTransform.offsetMin.x, -offsetY);
            img.rectTransform.offsetMax = new Vector2(img.rectTransform.offsetMax.x, -offsetY);
        }


        public static List<T> GetProperties<T>(this object obj)
        {
            List<T> Props = new List<T>();
            foreach (var prop in obj.GetType().GetProperties()){
                if(prop.GetType() == typeof(T)){
                    Props.Add((T)prop.GetValue(obj, null));
                }
            }
            return Props;
        }
    }
}
