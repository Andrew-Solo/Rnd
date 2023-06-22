import Model from "./Model";
import Collection from "../Collection";
import Unit from "./Unit";
import {client} from "../../data/Client";
import {store} from "../Store";

export default class Module extends Model {
  version: string
  readonly creatorId: string
  mainId: string | null
  system: boolean
  default: boolean
  hidden: boolean
  order: number

  get units(): Collection<Unit> {
    if (!!this._units) return this._units;
    this._units = new Collection<Unit>(client.units(store.user, this.name), Unit);
    this._units.load();
    return this._units;
  }

  private _units: Collection<Unit> | null;

  constructor(data: {id: string, name: string, path: string, creatorId: string, [key:string]: any}) {
    super(data);
    this.version = data.version ?? "0.1.0";
    this.creatorId = data.creatorId;
    this.mainId = data.mainId ?? null;
    this.system = data.system ?? false;
    this.default = data.default ?? false;
    this.hidden = data.hidden ?? false;
    this.order = data.order ?? 8;
    this._units = null;
    // makeAutoObservable(this, {
    //   creatorId: false,
    // }, { autoBind: true });
  }
}