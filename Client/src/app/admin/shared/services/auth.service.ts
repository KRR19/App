import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {UserModel} from '../../../models/UserModel';

@Injectable()
export class AuthService {
  constructor(private  http: HttpClient) {}
  get token(): string {
    return  '';
  }

  login(user: UserModel): Observable<any> {
    return  this.http.post('', user);
  }
  logout() {

  }
  isAuthenticated(): boolean {
    return !!this.token;
  }

  private setToken() {

  }
}
