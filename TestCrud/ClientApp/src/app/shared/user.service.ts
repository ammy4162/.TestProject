import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpOptions } from './Models/httpOptions';
import { SelectItem } from './Models/SelectItem';
import { UserDetail } from './user-detail.model';
import * as _ from 'lodash';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  httpOptions: HttpOptions;
  apiBaseUrl: string;
  formData: UserDetail;
  baseUrl = environment.baseUrl;
  list: UserDetail[];
  onUserChanged: BehaviorSubject<any>;

  constructor(private httpClient: HttpClient,
    @Inject("BASE_URL") private _baseAddress: string) {
    this.apiBaseUrl = this._baseAddress + "api/v1/";
    this.httpOptions = new HttpOptions();
    this.onUserChanged = new BehaviorSubject([]);
  }

  onUserEdit(userDetails: any) {
    this.onUserChanged.next(userDetails);
  }

  public save(profile: UserDetail) {
    let url = this.apiBaseUrl + "user/save";
    console.log("URL", url);
    return this.httpClient.post(url, profile);
  }

  getUserList() {
    let url = this.apiBaseUrl + "user/getAll";
    return this.httpClient.get(url);
    // //this.httpClient.get(url)
    //   .toPromise()
    //   .then(res => this.list = res as UserDetail[]);
  }

  getUser(userId: string) {
    let url = this.apiBaseUrl + "user/" + userId;
    return this.httpClient.get(url);
  }

  getSkillType() {
    let url = this.apiBaseUrl + "user/skillType";
    return this.httpClient.get(url);
  }

  deleteUser(userId: string) {
    let url = this.apiBaseUrl + "user/" + userId;
    return this.httpClient.delete(url);
  }
}
