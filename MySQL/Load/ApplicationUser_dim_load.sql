-- use ADB1;
insert into ADB1.ApplicationUser_dim (Email,groupmembership)
select  ApplicationUser.Email, 
        ApplicationUser.groupmembership
from UiA_DB.applicationUser;

-- use ADB1;
-- select * from ApplicationUser_dim;
