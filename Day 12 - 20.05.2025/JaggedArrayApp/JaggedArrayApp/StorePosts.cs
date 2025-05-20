using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaggedArrayApp
{
    public class StorePosts
    {
        GetUserInput getUserInput = new GetUserInput();
        private object GetData(string message, string dataType)
        {
            object result = getUserInput.SingleUserInput(message, dataType);
            return result;
        }
        public void InsertIntoArray()
        {
           
            int UserCount = (int)GetData("Please Enter the Users count: ","Integer");
            object[][][]? Posts = new object[UserCount][][];

            for (int i = 0; i < UserCount; i++)
            {
                Console.WriteLine($"user {i+1} posts");
                int PostCount = (int)GetData($"Please Enter the Post count for user:{i+1} = ", "Integer"); ;

                Posts[i] = new object[PostCount][];
                for(int j = 0; j < PostCount; j++)
                {
                    string Caption = (string)GetData($"Please Enter the Caption for Post {j + 1}:", "String");
                    int Likes = (int)GetData($"Please Enter the Likes for Post {j + 1}:", "Integer");
                    Posts[i][j] = new object[] { Caption, Likes };
                    //Console.WriteLine($"{Posts[i][j][0]},{Posts[i][j][1]}");
                }
            }

            foreach (var post in Posts)
            {
                if (post == null) continue;
                foreach (var pair in post)
                {
                    Console.WriteLine($"{pair[0]},{pair[1]}");
                }
            }

        }
    }
}
