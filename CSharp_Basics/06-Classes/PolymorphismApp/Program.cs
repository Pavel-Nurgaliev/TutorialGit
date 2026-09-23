using MehodOverloadingProject;

Mercedess mercedess = new Mercedess();
Lada lada = new Lada();

Vehicle[] vehs = new Vehicle[] { mercedess, lada };

foreach (Vehicle v in vehs)
{
    v.GetVehicleName();
}