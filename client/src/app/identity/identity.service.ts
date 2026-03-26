import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ActiveAccount } from '../shared/Models/ActiveAccount';
import { ResetPassword } from '../shared/Models/ResetPassword';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  baseURL=environment.baseURL
  constructor(private http:HttpClient) { }

  register(form:any){
    return this.http.post(this.baseURL+"account/register",form)
  }
  login(form:any){
    return this.http.post(this.baseURL+"account/login",form)
  }
  active(param:ActiveAccount){
    return this.http.post(this.baseURL+"account/active-account",param)
  }
  forgetPassword(email:string){
   return this.http.get(this.baseURL+`account/send-email-forget-password?email=${email}`)
  }
  resetPassword(param:ResetPassword){
   return this.http.post(this.baseURL+"account/reset-password",param)
  }
}
