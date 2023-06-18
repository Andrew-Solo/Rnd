import User from "../models/User";
import Provider from "./Provider";
import Module from "../models/Module";

export  class Client {
  constructor(host: string) {
    this.host = host;
  }

  users = () => new Provider<User>({host: this.host, url: "users"});
  modules = () => new Provider<Module>({host: this.host, url: "modules"});

  host: string;
}

export const client = new Client("http://localhost:5083");

