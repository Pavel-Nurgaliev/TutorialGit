using CompositionApp;

DbMigrator dbMigrator = new DbMigrator(new Logger());

dbMigrator.Migrate();

Installer install = new Installer(new Logger());

install.Install();