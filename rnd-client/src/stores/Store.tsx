import Session from "./Session";
import Modules from "./Modules";

export default class Store {
  session: Session = new Session();
  modules: Modules = new Modules(this);
}