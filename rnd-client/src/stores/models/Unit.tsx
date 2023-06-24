import Model from "./Model";
import Collection from "../Collection";
import {client} from "../../data/Client";
import {store} from "../Store";
import Instance from "./Instance";

export default class Unit extends Model {
  readonly moduleId: string
  default: boolean
  hidden: boolean
  order: number

  get instances(): Collection<Instance> {
    if (!!this._instances) return this._instances;
    this._instances = new Collection<Instance>(client.instances(store.user, this.path), Instance);
    this._instances.load();
    return this._instances;
  }

  private _instances: Collection<Instance> | null;

  constructor(data: {id: string, name: string, path: string, moduleId: string, [key:string]: any}) {
    super(data);
    this.moduleId = data.moduleId;
    this.default = data.default ?? false;
    this.hidden = data.hidden ?? false;
    this.order = data.order ?? 8;
    this._instances = null;
  }
}