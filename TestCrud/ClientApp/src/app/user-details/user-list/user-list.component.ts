import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { toastrConfig } from 'src/app/shared/Constants';
import { UserDetail } from 'src/app/shared/user-detail.model';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styles: []
})
export class UserListComponent implements OnInit {
  public response: any[] = [];
  public length: number;
  public userProfile: UserDetail;

  constructor(private _userService: UserService, private _toastrService: ToastrService) { }

  ngOnInit() {
    this.refreshList();

  }

  refreshList() {
    this.getUserList();
  }

  getUserList() {
    this._userService.getUserList().subscribe(
      (data) => {
        let arrayData = [];
        for (const prop in data) {
          var date = null;
          if (data[prop].dob != null) {
            date = (data[prop].dob).split('T')[0];
          }
          let jsonObject = {
            Id: data[prop].id.toString(),
            Name: data[prop].name,
            SkillType: data[prop].skillType,
            Designation: data[prop].designation,
            DOB: date
          };
          arrayData.push(jsonObject);
        }
        this.response = arrayData as UserDetail[];
        this.length = this.response.length;
      },
      (err) => {
        this.response = err.Name;
        console.log(err);
      }
    );
  }

  getUser(userDetails: UserDetail) {
    this._userService.formData = Object.assign({}, userDetails);
  }

  deleteUser(userDetails: any) {
    if (confirm('Are you sure to delete this record ?')) {
      this._userService.deleteUser(userDetails.Id).subscribe(
        (response: any) => {
          if (!response.error) {
            this._toastrService.success(response.message, "", toastrConfig);
            this.refreshList();
          } else {
            this._toastrService.error(response.message, "", toastrConfig);
          }
        },
        (error) => {
          this.refreshList();
          console.log("Error received");
          console.log(error);
        }
      );

    }
  }

}
