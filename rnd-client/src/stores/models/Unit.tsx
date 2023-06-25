import Model from "./Model";
import Collection from "../Collection";
import {client} from "../../data/Client";
import {store} from "../Store";
import Instance from "./Instance";
import Field from "./Field";

export default class Unit extends Model {
  readonly moduleId: string
  default: boolean
  hidden: boolean

  get instances(): Collection<Instance> {
    if (!!this._instances) return this._instances;
    this._instances = new Collection<Instance>(client.instances(store.user, this.path), Instance);
    this._instances.load();
    return this._instances;
  }

  private _instances: Collection<Instance> | null;

  get fields(): Collection<Field> {
    if (!!this._fields) return this._fields;
    this._fields = new Collection<Field>(client.fields(store.user, this.moduleId, this.id), Field);
    this._fields.load();
    return this._fields;
  }

  private _fields: Collection<Field> | null;

  constructor(data: {id: string, name: string, path: string, moduleId: string, [key:string]: any}) {
    super(data);
    this.moduleId = data.moduleId;
    this.default = data.default ?? false;
    this.hidden = data.hidden ?? false;
    this._instances = null;
    this._fields = null;
  }
}