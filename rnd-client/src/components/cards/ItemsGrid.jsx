import {Grid} from "@mui/material";
import ItemCard from "./ItemCard";
import NewCard from "./NewCard";

export default function ItemsGrid({data}) {
  return (
    <Grid container spacing={2} >
      <Grid item xs="auto">
        <NewCard/>
      </Grid>
      {data.map(item => (
        <Grid key={item.name} item xs="auto">
          <ItemCard {...item}/>
        </Grid>
      ))}
    </Grid>
  );
}