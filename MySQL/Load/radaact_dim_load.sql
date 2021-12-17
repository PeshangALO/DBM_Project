-- use ADB1;
insert into ADB1.radacct_dim (username,acctstarttime,acctstoptime,acctinputoctets)
select  radacct.username,
        radacct.acctstarttime,
        radacct.acctstoptime,
        radacct.acctinputoctets
from radius.radacct;

-- TRUNCATE TABLE radaact_dim;
-- use ADB1;
-- select * from radacct_dim;


