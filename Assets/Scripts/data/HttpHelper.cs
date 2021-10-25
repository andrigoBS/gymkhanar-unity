using UnityEngine;

namespace DataHelpers
{
	/*
	 *	Class used for connection with backend
	 *  Here you should rewrite the methods for connect with your backend
	 */
    public class HttpHelper
    {
        public static Level getLevel(string token)
        {
            if(Mockup.levelPointer >= Mockup.levels.Length) Mockup.levelPointer = 0;
			return Mockup.levels[Mockup.levelPointer++];	
        }

        public static Tip getTip(string token, int id)
        {
            return Mockup.tips[id];
        }

		public static Tracking getTracking(string token, int id)
		{
			return Mockup.trackings[id];
		}

		public static Question getQuestion(string token, int id)
		{
			return Mockup.questions[id];
		}
    }

	/*
	 *  Mockup class used for test
	 *  Delete this class if you use http request
	 */
	public class Mockup
	{
		public static int levelPointer = 0;

		public static Level[] levels = new Level[]{
			Level.fromJSON("{"+
                           "\"id\": 0,"+
                           "\"levelName\":\"Level 1\","+
						   "\"sceneSequence\":[\"Tip\",\"Tracking\",\"Question\",\"ChestReward\"],"+
                           "\"tipId\": 0,"+
                           "\"questionId\": 0,"+
                           "\"trackingId\": 0"+
                           "}"
			),
			Level.fromJSON("{"+
                           "\"id\": 1,"+
                           "\"levelName\":\"Level 2\","+
						   "\"sceneSequence\":[\"Tip\",\"Tracking\",\"Question\",\"ChestReward\"],"+
                           "\"tipId\": 1,"+
                           "\"questionId\": 0,"+
                           "\"trackingId\": 1"+
                           "}"
			)
		};

		public static Tip[] tips = new Tip[]{
			Tip.fromJSON("{" +
                         "\"id\": 0," +
                         "\"title\": \"Grave a Dica\"," +
                         "\"subtitle\": \"Procure por imagens de um baú\"," + 
                         "\"body\": \"Sou uma estrutura de repetição. Quem sou eu?\""+
                         "}"
			),
			Tip.fromJSON("{" +
                         "\"id\": 1," +
                         "\"title\": \"Grave a Dica\"," +
                         "\"subtitle\": \"Procure por um cômodo\"," + 
                         "\"body\": \"Sou uma estrutura de repetição. Quem sou eu?\""+
                         "}"
			)
		};

		public static Tracking[] trackings = new Tracking[]{
			Tracking.fromJSON("{" +
                              "\"id\": 0," +
							  "\"type\": \"image\"," +
                              "\"url\": \"http://192.168.3.8:5000/files/tracking/chest.wtc\"," +
                              "\"assetName\": \"Chest\"" + 
                              "}"
			),
			Tracking.fromJSON("{" +
                              "\"id\": 1," +
						      "\"type\": \"object\"," +
                              "\"url\": \"http://192.168.3.8:5000/files/tracking/room.wto\"," +
                              "\"assetName\": \"Chest\"" + 
                              "}"
			)
		};

		public static Question[] questions = new Question[]{
			Question.fromJSON("{" +
							  "\"id\": 0," +
							  "\"text\": \"Sou utilizado quando você precisa repetir várias vezes um ou mais comandos. " +
									      "Dependendo da situação, posso não ser executado. "+
									      "Não sei quantas repetições serão executadas.\"," +
							  "\"options\": [" +
							     "{" +
									 "\"text\": \"Para...faça (for)\","+
									 "\"correction\": \"Ops. A estrutura para...faça (for) tem comportamento de repetição de "+
													  "um bloco de sentenças por um número específico de interações. "+
													  "Um para...faça sempre está acompanhado de uma variável contadora que "+
													  "armazena quantas vezes o bloco de sentenças da estrutura de repetição "+
													  "deve ser executada.\","+
									 "\"isCorrect\": false" +
								 "}," +
								 "{" +
									 "\"text\": \"Enquanto...faça (while)\","+
									 "\"correction\": \"Parabéns! Você acertou! O enquanto...faça (while) é a estrutura de "+
												      "repetição mais simples. Ele repete a execução de um bloco de sentenças "+
													  "enquanto uma condição permanecer verdadeira. Na primeira vez que a "+
													  "condição se tornar falsa, o while não repetirá a execução do bloco. "+
													  "Pode acontecer de não ser executada.\","+
									 "\"isCorrect\": true" +
								 "}," +
								 "{" +
									 "\"text\": \"Faça...enquanto (do while)\","+
									 "\"correction\": \"Ops! A estrutura faça...enquanto (do while) tem o comportamento de "+
													  "repetição de comandos, porém a condição é verificada após executar o "+
													  "bloco de instruções correspondente. Ou seja, é necessário rodar os "+
													  "comandos pelo menos uma vez para realizar a verificação da condição.\","+
									 "\"isCorrect\": false" +
								 "}" +
							   "]" +
							   "}"
			)
		};
	}   
}
