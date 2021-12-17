-- use ADB1;
insert into ADB1.radreply_dim (username,value)
select  radreply.username,
        radreply.value
from radius.radreply;
-- WHERE NOT (value LIKE '%psk%');

-- TRUNCATE TABLE radreply_dim;
-- use ADB1;
-- select * from radreply_dim;


