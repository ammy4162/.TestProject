import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@Component({
  selector: 'app-setup-user',
  templateUrl: './setup-user.component.html',
  styleUrls: ['./setup-user.component.css'],
})
export class SetupUserComponent implements OnInit {

  basicForm: FormGroup;

  constructor() { }

  ngOnInit() {
  }

}
