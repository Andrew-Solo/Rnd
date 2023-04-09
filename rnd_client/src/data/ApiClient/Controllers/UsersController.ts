import Controller, {ErrorCallback, SuccessCallback} from "@/data/ApiClient/Controllers/Controller";

class UsersController extends Controller {
  readonly name: string = "users";

  public get(id: string, onSuccess: SuccessCallback<User>, onError: ErrorCallback) {
    this.getRequest({}, id, onSuccess, onError);
  }

  public login(login: string, password: string, onSuccess: SuccessCallback<User>, onError: ErrorCallback) {
    this.getRequest({login, password}, "", onSuccess, onError);
  }

  public create(form: UserForm, onSuccess: SuccessCallback<User>, onError: ErrorCallback) {
    this.updateRequest(form, "", onSuccess, onError);
  }

  public update(id: string, form: UserForm, onSuccess: SuccessCallback<User>, onError: ErrorCallback) {
    this.createRequest(form, id, onSuccess, onError);
  }
}

export interface User {
  _id: string,
  login: string,
  email: string,
  role: UserRole,
  registered: Date,
  _gameIds: string[],
  games: string[]
}

export interface UserForm {
  login: string,
  email: string,
  password: string,
  role: UserRole,
  discordId: number
}

export enum UserRole {
  Guest = "Guest",
  User = "User",
  Admin = "Admin"
}

export class Guest implements User {
  _id = "";
  login = "Гость";
  email = "Гость";
  role = UserRole.Guest;
  _gameIds = [];
  games = [];
  registered = new Date();
}

export default UsersController;