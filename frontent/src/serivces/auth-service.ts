import axios, { AxiosResponse } from 'axios';
import { UserManager, OidcClientSettings, User, Profile } from 'oidc-client';

const url = 'https://localhost:5698';
const ApplicationName = 'react_auth';


export enum AuthenticationResultStatus {
  success
}

interface AuthenticationResult {
  status:  AuthenticationResultStatus,
  state: any
}

class AuthenticationService {
  private _userManager: UserManager | null = null;
  private _user: User | null = null;
  private _isAuthenticated:boolean = false;

  private async retrieveConfiguration(): Promise<AxiosResponse<OidcClientSettings>> {
    return axios({
      url: `${url}/_configuration/${ApplicationName}`
    });
  }

  public async isAuthenticated():Promise<boolean> {
    const user = await this.getUser();
    return !!user;
  }

  public async signIn() {
    await this.ensureUserManagerCreated();
    await this._userManager!.signinRedirect();
  }

  public async completeSignIn(url:string): Promise<AuthenticationResult>{
    try {
      await this.ensureUserManagerCreated();
      const user =  await this._userManager!.signinCallback(url);
      this._user = user;
      return { status: AuthenticationResultStatus.success, state: user && user.state };
    } catch (err){
      console.log(err);
      throw err;
    }
  }

  public async getAccessToken() {
    await this.ensureUserManagerCreated();
    const user = await this._userManager!.getUser();
    return user && user.access_token;
  };

  public async getUser(): Promise<Profile | undefined> {
    if(this._user && this._user.profile){
      return this._user.profile;
    }

    await this.ensureUserManagerCreated();
    this._user = await this._userManager!.getUser();
    return this._user?.profile;
  }


  public async ensureUserManagerCreated(): Promise<void> {
    if (this._userManager === null) {
      const {data: settings} = await this.retrieveConfiguration();
      this._userManager = new UserManager(settings);
    }
  }
}

const authenticationService = new AuthenticationService();
export default authenticationService;
