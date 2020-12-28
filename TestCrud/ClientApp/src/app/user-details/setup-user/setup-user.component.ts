import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbCalendar, NgbDatepicker, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { SelectItem } from 'src/app/shared/Models/SelectItem';
import { UserDetail } from 'src/app/shared/user-detail.model';
import { UserService } from 'src/app/shared/user.service';
import { takeUntil } from "rxjs/operators";
import { Subject } from 'rxjs';
import { Router } from '@angular/router';
import { toastrConfig } from 'src/app/shared/Constants';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-setup-user',
  templateUrl: './setup-user.component.html',
  styleUrls: ["./setup-user.component.scss"],
})
export class SetupUserComponent implements OnInit {

  public userProfile: UserDetail;
  private _unsubscribeAll: Subject<any>;
  skillTypeValue: any;
  skillType: string;

  public skillTypes: any;
  countries = [
    { id: 1, name: "United States" },
    { id: 2, name: "Australia" },
    { id: 3, name: "Canada" },
    { id: 4, name: "Brazil" },
    { id: 5, name: "England" }
  ];
  selectedValue = null;
  constructor(private _userService: UserService, private _toastrService: ToastrService) { }

  ngOnInit() {
    this.resetForm();
    this.getSkillTypes();
    this.getUser();
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.resetForm();
    this._userService.formData = {
      Id: '',
      Name: '',
      Designation: '',
      DOB: null,
      SkillType: ''
    }
  }

  getUser() {
    this._userService.onUserChanged.subscribe(response => {
      console.log("getUser", response);
      if (response.length != 0) {
        this._userService.formData = <UserDetail>response;
      }
    })
  }

  onSubmit(form: NgForm) {
    if (this._userService.formData.Id != null && this._userService.formData.Id != "" && this._userService.formData.Id != undefined) {
      this.userProfile = form.value;
      this.userProfile.Id = this._userService.formData.Id;
    }
    else {
      this.userProfile = form.value;
    }
    this._userService.save(this.userProfile).subscribe(
      (response: any) => {
        if (response != null && !response.error) {
          this._toastrService.success(response.message, "", toastrConfig);
          this.refreshList();
        }
        else {
          this._toastrService.success(response.message, "", toastrConfig);
        }
      },
      (error) => {
        console.log("Error received");
        console.log(error);
      }
    );
  }

  getSkillTypes() {
    this._userService.getSkillType().subscribe(result => {
      this.skillTypes = result;
      console.log("St", this.skillTypes);
    });
  }

  refreshList() {
    location.reload();
  }

}
