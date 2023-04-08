import Controller from "@/api/Controllers/Controller";
import {DataResponse, Response} from "@/api/Controllers/Controller"

class UsersController extends Controller {
  readonly name: string = "users";

  public login(login: string, password: string, onSuccess: (response: DataResponse<User>) => void, onError: (response: Response) => void) {
    this.getRequest({login, password}, onSuccess, onError);
  }
}

export interface User {
  _id: string,
  login: string,
  email: string,
  role: string,
  registered: Date,
  _gameIds: string[],
  games: string[]
}

export default UsersController;