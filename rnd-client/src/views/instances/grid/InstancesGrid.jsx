import {Grid} from "@mui/material";
import InstanceCard from "./InstanceCard";
import NewInstance from "./NewInstance";

export default function InstancesGrid({instances}) {
  return (
    <Grid container spacing={2} >
      <Grid item xs="auto">
        <NewInstance/>
      </Grid>
      {instances.map(instance => (
        <Grid key={instance.name} item xs="auto">
          <InstanceCard {...instance}/>
        </Grid>
      ))}
    </Grid>
  );
}