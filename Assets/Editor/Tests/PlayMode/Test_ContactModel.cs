using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test_ContactModel
    {
        [UnityTest]
        public IEnumerator Test_ContactSimple()
        {
            yield return null;
            yield return null;

            ContactController.Instance.SetAllContactsFromDb(false);

            if (ContactController.Instance.AllContacts.Count == 0)
            {
                List<ContactModel> contacts = ContactModel.CreateRandom(10);
                contacts.ForEach(c => Db.ContactRepo.Create(c));
            }

            ContactController.Instance.SetAllContactsFromDb(false);

            Assert.True(ContactController.Instance.AllContacts.Count > 0);

            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_ContactSimple2()
        {
            Assert.True(ContactModel.CreateRandom(10).Count == 10);

            yield return null;
        }
    }
}
