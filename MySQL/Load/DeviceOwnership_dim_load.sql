-- use ADB1;
insert into ADB1.DeviceOwnership_dim (ActiveUntil,DateRegistered,State,OwnerEmail,DeviceName,MAC,Vlan)
select  DeviceOwnership.ActiveUntil,
        DeviceOwnership.DateRegistered,
        DeviceOwnership.State,
        DeviceOwnership.OwnerEmail,
        DeviceOwnership.DeviceName,
        DeviceOwnership.MAC,
        DeviceOwnership.VlanAlias
from UiA_DB.DeviceOwnership;


-- TRUNCATE TABLE radaact_dim;
-- use ADB1;
-- select * from DeviceOwnership_dim;