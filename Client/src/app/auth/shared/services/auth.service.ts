import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {UserModel} from '../../../models/UserModel';
import {Responsemodel} from '../../../models/Responsemodel';
import {LogInResponceModel} from '../../../models/LogInResponceModel';

@Injectable()
export class AuthService {

  constructor(private  http: HttpClient) {}

  get token(): string {
    return  '';
  }

  public async SingUp(user: UserModel): Promise<Responsemodel> {
    const result: Responsemodel = await this.http.post<Responsemodel>('https://localhost:44378/api/Account/Register', user).toPromise();
    return result;
  }

  public async SingIn(user: UserModel): Promise<LogInResponceModel> {
    const response: LogInResponceModel = await this.http.post<LogInResponceModel>('https://localhost:44378/api/Account/SingIn', user)
      .toPromise();
    if (response.isValid) {
      this.setToken(response);
    }
    return response;
  }
  logout() {
      localStorage.clear();
  }
  isAuthenticated(): boolean {
    return !!this.token;
  }
  public async  forgotPassword(user: UserModel): Promise<Responsemodel> {

    const result: Responsemodel = await this.http.post<Responsemodel>('https://localhost:44378/api/Account/ForgotPassword', user).
    toPromise();
    return result;
  }

  private setToken(response: LogInResponceModel | null) {
    localStorage.setItem('accessToken', response.accessToken);
    localStorage.setItem('refreshToken', response.refreshToken);
    localStorage.setItem('Role', response.role);
  }
}
