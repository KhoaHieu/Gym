using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Utils
{
    class FirebaseHelper
    {
        public readonly static FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDxlIINJoFZUH1CQA7alxIjW5g1Ivs3ZtQ"));

        public static async Task<bool> loginWithEmailAndPasswordAsync(string email, string password)
        {
            try
            {
                await firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, password);
                return true;
                //Debug.WriteLine("Success");
            }
            catch (Exception)
            {
                return false;
                //Debug.WriteLine("Error");
            }
        }


        public static async Task<bool> resetPassword(string email)
        {
            try
            {
                await firebaseAuthProvider.SendPasswordResetEmailAsync(email);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
