using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoiceExpander
{
    public static class Extentions
    {
        public static void DisplayElement(this IEnumerable<object> query)
        {
            foreach (var q in query)
            {
                Console.WriteLine(string.Join(" ", q));
            }
        }
        /// <summary>
        /// 結果表示用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        public static void DisplayElement<T>(this IEnumerable<T[]> query)
        {
            foreach (var q in query)
            {
                Console.WriteLine(string.Join(" ", q));
            }
        }

        /// <summary>
        /// 選択肢の列挙の全組み合わせを展開
        /// choices {1,2,3}⇒ 戻り値{1,2,3}{1,3,2}{2,1,3}{2,3,1}{3,1,2}{3,2,1}
        /// </summary>
        /// <typeparam name="T">型引数</typeparam>
        /// <param name="choices">選択肢の列挙</param>
        /// <returns>展開結果</returns>
        public static IEnumerable<T[]> Expand<T>(this IEnumerable<T> choices)
        {
            //重複あり
            if (choices.Count() != choices.Distinct().Count())
            {
                throw new Exception("has duplicate value");
            }

            //選択結果は空からスタート
            var choosed = new T[] { };

            //展開開始
            var query = Expand(choices, choosed);
            return query;
        }

        /// <summary>
        /// 選択肢の列挙の全組み合わせを展開(再帰)
        /// </summary>
        /// <typeparam name="T">型引数</typeparam>
        /// <param name="choices">選択肢の列挙</param>
        /// <param name="choosed">選択済要素の列挙</param>
        /// <returns></returns>
        private static IEnumerable<T[]> Expand<T>(IEnumerable<T> choices, IEnumerable<T> choosed)
        {
            //選択肢がなくなったら終了
            if (!choices.Any()) { yield return choosed.ToArray(); }

            foreach (var choice in choices)
            {
                //選択肢から1つ選んで、選択済要素に入れる
                var newChoosed = choosed.Concat(new T[] { choice });

                //選択肢から削除
                var newChoices = choices.Where(v => !choice.Equals(v));

                //新たな選択肢と選択済要素で再帰
                var query = Expand(newChoices, newChoosed);
                foreach (var qq in query)
                {
                    //配列を順次返す
                    yield return qq.ToArray();
                }
            }
        }
    }
}
