using System;
using GenFu;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class DatabaseInitializer
    {
        public static void Initialize(DatabaseContext_UiA context_UiA, DatabaseContext_Rad context_Rad, bool development)
        {
            // Run migration if we're not in development mode
            if (development)
            {
                context_Rad.Database.EnsureDeleted();
                context_Rad.Database.EnsureCreated();

                context_UiA.Database.EnsureDeleted();
                context_UiA.Database.EnsureCreated();
            } else { 
                context_Rad.Database.Migrate();
                context_UiA.Database.Migrate();
                return;
            }

    
            context_UiA.AspNetRoles.Add(new AspNetRoles {Name = "admin", NormalizedName = "ADMIN"});
            context_UiA.AspNetRoles.Add(new AspNetRoles {Name = "employee", NormalizedName = "EMPLOYEE"});
            context_UiA.AspNetRoles.Add(new AspNetRoles {Name = "student", NormalizedName = "STUDENT"});

            var random = new Random();
            
            // 1. Create 10 stk vlan
            // 2. generate 1000 users, with group membership between 0 and 2
            // 3. 10000 stk DeviceOwnership, starttime låst? endres til random mellom 2018 frem til nå. hvis unpersonal: ingen enddate. hvis personal: enddate en mnd frem.
            // 4. random 0 - 10 per device, starttime random mellom 2018 frem til neste år, de som er over dags dato blir statt til null.



        
            // context_UiA.Vlan.Add(new Vlan {SSID = "Eduroam", VlanAlias = "Student-1", VlanName = "124", AccessibleByUser = Vlan.UserType.Student});

            // 1. Create 10 stk vlan
            for (int i = 1; i < 11; i++)
            {
                var randomNumber = random.Next(1, 4);
                var userType = Vlan.UserType.Student;
                if (randomNumber == 1)
                { 
                    userType = Vlan.UserType.Employee;
                } else if (randomNumber == 2)
                {
                    userType = Vlan.UserType.Admin;
                } 
                var number = 120 + i;
                context_UiA.Vlan.Add(new Vlan {SSID = "Eduroam", VlanAlias = "Vlan"+i.ToString(), VlanName = number.ToString(), AccessibleByUser = userType});
            }

            context_UiA.SaveChanges();

            
            // 2. generate 1000 users, with group membership between 0 and 2
            

            // generate user with 10 devices, and an email and group between 0 and 2
                // generate a device using a random vlan that has been created and a random MAC address

                    // generate between 0 and 10 sessions for device



  
            var number_of_users = 1000;
            var number_of_devices = 10;

            var dummyUsers = A.ListOf<ApplicationUser>(number_of_users);
            var dummySession = A.ListOf<Radacct>(70000);


            int k= 0;
            int l= 0;
            // int j= 0;
            int m= 0;


            dummyUsers.ForEach(user =>
            {
                user.GroupMembership = (ApplicationUser.Group) random.Next(0, 3);
                user.Email = m+dummyUsers[l++].Email;
                user.NormalizedEmail = user.Email.ToUpper();;
                var Osername = user.UserName;
                user.UserName = Osername+m +random.Next(1, 99999).ToString();
                user.NormalizedUserName = user.UserName.ToUpper();

                var dummyDevices = A.ListOf<Device>(number_of_devices);
                var dummyDeviceOwnerships = A.ListOf<DeviceOwnership>(number_of_devices);

                k = 0;
                dummyDeviceOwnerships.ForEach(d =>
                {
                    var dummyDev = dummyDevices[k++];
                    dummyDev.Id = (uint) random.Next(1, 999999999)+(uint)m;
                    
                    dummyDev.MAC = $"{dummyDev.MAC}-{random.Next()}";
                    dummyDev.Password = dummyDev.MAC;
                    dummyDev.VlanAlias = context_UiA.Vlan.Find("Vlan"+random.Next(1, 11).ToString()).VlanAlias;

                    d.Vlan = context_UiA.Vlan.Find(dummyDev.VlanAlias);
                    d.VlanAlias = dummyDev.VlanAlias;
                    d.MAC = dummyDev.MAC;
                    d.DeviceId = dummyDev.Id;
                    d.Id = (int) dummyDev.Id;
                    d.OwnerEmail = user.Email;

                    var number = random.Next(1, 11);
                    for (int oh = 0; oh < number; oh++, m++)
                    {
                        var startTime = DateTime.Now.AddDays(-random.Next(1, 365));
                        var endTime = startTime.AddDays(random.Next(1, 400));
                        var time = endTime - startTime;
                        
                        //convert system time to uint
                        var startTimeUint = (uint) startTime.ToFileTimeUtc();
                        var endTimeUint = (uint) endTime.ToFileTimeUtc();

                        var acctsessiontime = startTimeUint - endTimeUint;
                        var acctinputoctets = random.Next(256, 3200000);
                        var acctoutputoctets = random.Next(256, 3200000);
                        // Console.WriteLine(endTime);
                        // Console.WriteLine(DateTime.Now);
                        if (endTime >= DateTime.Now) 
                        {
                            acctsessiontime = 0;
                            acctinputoctets = 0;
                            acctoutputoctets = 0;
                        }

                        context_Rad.Radacct.Add(new Radacct
                        {
                            //string .
                            Acctsessionid = startTimeUint.ToString()+m.ToString()+endTimeUint.ToString(),
                            //string .
                            Acctuniqueid = m.ToString(),
                            //string .
                            Username = dummyDev.MAC,
                            //string .
                            Groupname = dummySession[m].Groupname,
                            //string.
                            Realm = dummySession[m].Realm,
                            //string.
                            Nasipaddress = dummySession[m].Nasipaddress,
                            //string.
                            Nasportid = dummySession[m].Nasportid,
                            //string.
                            Nasporttype = dummySession[m].Nasporttype,
                            //DateTime? . .
                            Acctstarttime = startTime,
                            //DateTime? . .
                            Acctupdatetime = dummySession[m].Acctupdatetime,
                            //DateTime? . .
                            Acctstoptime = endTime,
                            //int? . .
                            Acctinterval = dummySession[m].Acctinterval,
                            //uint? . .
                            Acctsessiontime = acctsessiontime,
                            //string . .
                            Acctauthentic = dummySession[m].Acctauthentic,
                            //string . // 
                            ConnectinfoStart = dummySession[m].ConnectinfoStart,
                            //string // 
                            ConnectinfoStop = dummySession[m].ConnectinfoStop,
                            //long? . . 
                            Acctinputoctets = acctinputoctets,
                            //long? . .
                            Acctoutputoctets = acctoutputoctets,
                            //string . .
                            Calledstationid = dummySession[m].Calledstationid,
                            //string . .
                            Callingstationid = dummyDev.MAC,
                            //string . .
                            Acctterminatecause = "User-Request",
                            //string . .
                            Servicetype = dummySession[m].Servicetype,
                            //string //.
                            Framedprotocol = dummySession[m].Framedprotocol,
                            //string //.
                            Framedipaddress = dummySession[m].Framedipaddress,
                            //string //.
                            Framedipv6address = dummySession[m].Framedipv6address,
                            //string //.
                            Framedipv6prefix = dummySession[m].Framedipv6prefix,
                            //string .  .
                            Framedinterfaceid = dummySession[m].Framedinterfaceid,
                            //string  .
                            Delegatedipv6prefix = dummySession[m].Delegatedipv6prefix, 
                        }); 
        
                    };
                    context_Rad.SaveChanges();
                });

                context_UiA.ApplicationUser.Add(user);
                context_UiA.Device.AddRange(dummyDevices);
                context_UiA.DeviceOwnership.AddRange(dummyDeviceOwnerships);
                context_UiA.SaveChanges();
                
            });


            // for (int i = 0; i < 10; i++)
            // {
            //     user.GroupMembership = (ApplicationUser.Group)j;
            //     user.Email = emails[j++];

            //     var dummyDevices = A.ListOf<Device>(5);
            //     var dummyDeviceOwnerships = A.ListOf<DeviceOwnership>(5);
            //     // var dummyVlan = A.ListOf<Vlan>(5);

            //     k = 0;
            //     dummyDeviceOwnerships.ForEach(d =>
            //     {

            //         d.Vlan = new Vlan("IoT", $"IoT-{random.Next()}", random.Next(), Vlan.UserType.Student);

            //         var dummyDev = dummyDevices[k++];
            //         dummyDev.Id = (uint)(k + 5 * (j - 1));
            //         dummyDev.Op = ":=";
            //         dummyDev.MAC = $"{dummyDev.MAC} {random.Next()}";

            //         dummyDev.VlanAlias = d.Vlan.VlanAlias;

            //         d.DeviceId = dummyDev.Id;
            //         d.Id = (int)dummyDev.Id;
            //         d.OwnerEmail = user.Email;

            //         d.VlanAlias = d.Vlan.VlanAlias;
            //     });

            //     context_UiA.ApplicationUser.Add(user);
            //     context_UiA.Device.AddRange(dummyDevices);
            //     context_UiA.DeviceOwnership.AddRange(dummyDeviceOwnerships);
            //     context_UiA.SaveChanges();
            // };

            // Dummy for testing background task
            // var device = new Device
            // {
            //     Id = 500,
            //     MAC = "EXPIRED BIGMAC"
            // };
            // context_UiA.Device.Add(device);
            // context_UiA.SaveChanges();
            // context_UiA.DeviceOwnership.Add(new DeviceOwnership
            // {
            //     Id = 600,
            //     Device = device,
            //     DeviceId = device.Id,
            //     MAC = device.MAC,
            //     State = 0,
            //     ActiveUntil = DateTime.MinValue
            // });

        }
    }
}