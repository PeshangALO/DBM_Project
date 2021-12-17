-- use ADB1;
insert into ADB1.Vlan_dim (VlanName)
select  Vlan.VlanAlias
from UiA_DB.Vlan;


-- use ADB1;
-- select * from Vlan_dim;


