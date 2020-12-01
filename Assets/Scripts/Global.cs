using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Global
{
    /// <summary>
    /// Checks the provided values for being null and if any is null, it'll throw a detailed exception.
    /// I would prefer doing this using a custom attribute but this is a lot faster to code and I'm short on time.
    /// </summary
    /// <param name="caller">Usually this will be: this.gameObject</param>
    /// <param name="values">The properties you need checking. Example: TxtName, InputFieldSurname, MenuPrefab</param>
    public static void CheckNullValues(GameObject caller, params object[] values)
    {
        foreach (var item in values)
        {
            if (item == null)
            {
                StackFrame frame = new StackFrame(1);
                Type funcCallerType = frame.GetMethod().DeclaringType;
                throw new NullReferenceException(string.Format("Please assign all required Inspector fields in Scene '{0}'\nGameObject: {1}, Script: {2}", SceneManager.GetActiveScene().name, caller.name, funcCallerType));
            }
        }
    }
}
