using UnityEngine;
using UnityEngine.SceneManagement;

namespace Work.SYW._01_Scripts
{
   public class TitleButton : MonoBehaviour
   {
      public void InGame()
      {
         SceneManager.LoadScene(1);
      }

      public void Quit()
      {
         Application.Quit();
      }
   }
}
