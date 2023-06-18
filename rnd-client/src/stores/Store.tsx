import Session from "./Session";
import Modules from "./Modules";

export default class Store {
  constructor() {
    this.session = new Session();
    this.modules = new Modules(this);
    this.modules.syncModules();
  }

  session: Session
  modules: Modules
}