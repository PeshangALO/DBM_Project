--- Load Fact table
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

--- Load application_dim table
insert into ADB1.ApplicationUser_dim (Email,groupmembership)
select  ApplicationUser.Email, 
        ApplicationUser.groupmembership
from UiA_DB.applicationUser;

--- Load ownership_dim table
insert into ADB1.DeviceOwnership_dim (ActiveUntil,DateRegistered,State,OwnerEmail,DeviceName,MAC,Vlan)
select  DeviceOwnership.ActiveUntil,
        DeviceOwnership.DateRegistered,
        DeviceOwnership.State,
        DeviceOwnership.OwnerEmail,
        DeviceOwnership.DeviceName,
        DeviceOwnership.MAC,
        DeviceOwnership.VlanAlias
from UiA_DB.DeviceOwnership;

--- Load Vlan_dim table
insert into ADB1.Vlan_dim (VlanName)
select  Vlan.VlanAlias
from UiA_DB.Vlan;

--- Load radreply_dim table
insert into ADB1.radreply_dim (username,value)
select  radreply.username,
        radreply.value
from radius.radreply;

--- Load radacct_dim table
insert into ADB1.radacct_dim (username,acctstarttime,acctstoptime,acctinputoctets)
select  radacct.username,
        radacct.acctstarttime,
        radacct.acctstoptime,
        radacct.acctinputoctets
from radius.radacct;


