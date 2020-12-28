import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject } from '@angular/core';

export class HttpOptions {

  constructor() {

  }

  getHttpOptions() {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      }),
    };
    return httpOptions;
  }

  getDeleteHttpOptions(profileId: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        entityIds: profileId,
        //ss
      }),
    };
    return httpOptions;
  }
}
