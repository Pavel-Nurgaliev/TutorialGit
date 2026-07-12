using TraficLightStateMachine;

Console.WriteLine(LightHelper.Next(Light.Red) == Light.Green);
Console.WriteLine(LightHelper.Next(Light.Yellow) == Light.Red);

var p = Permissions.Read | Permissions.Write; //its 3 because of bit | operator. 001 | 010 = 011

Console.WriteLine(PermissionsHelper.CanWrite(p));

Console.WriteLine(p.HasFlag(Permissions.Execute));