-- use ADB1;
insert into ADB1.Fact_table (radreply_dim,radaact_dim,ApplicationUser_dim,DeviceOwnership_dim,Vlan_dim)
select  radreply.id,
        radacct.radacctid,
        ApplicationUser.id, 
        DeviceOwnership.id,
        Vlan.VlanAlias
from radius.radreply, UiA_DB.Vlan, radius.radacct, UiA_DB.deviceOwnership, UiA_DB.applicationUser
where radreply.username = DeviceOwnership.MAC
and DeviceOwnership.VlanAlias = Vlan.VlanAlias
and DeviceOwnership.owneremail = ApplicationUser.Email
and radacct.username = DeviceOwnership.MAC
group by Vlan.VlanAlias,
        ApplicationUser.id,
        DeviceOwnership.id,
        radreply.id,
        radacct.radacctid;

-- use ADB1;
-- select * from Fact_table;


