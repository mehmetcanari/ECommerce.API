﻿namespace OnlineStoreWeb.API;

public interface IDependencyContainer
{
    void RegisterCoreDependencies();
    void LoadValidationDependencies();
}