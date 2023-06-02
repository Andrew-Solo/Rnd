import {Grid} from "@mui/material";
import HeroCard from "./HeroCard";
import HeroButton from "./HeroButton";

export default function HeroGrid({data}) {
  return (
    <Grid container spacing={2} >
      <Grid item xs="auto">
        <HeroButton/>
      </Grid>
      {data.map(item => (
        <Grid key={data.title} item xs="auto">
          <HeroCard image={item.image} title={item.title} subtitle={item.subtitle}/>
        </Grid>
      ))}
    </Grid>
  );
}