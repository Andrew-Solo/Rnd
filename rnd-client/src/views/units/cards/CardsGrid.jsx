import {Grid} from "@mui/material";
import ItemCard from "./ItemCard";
import NewCard from "./NewCard";

export default function CardsGrid({tokens}) {
  return (
    <Grid container spacing={2} >
      <Grid item xs="auto">
        <NewCard/>
      </Grid>
      {tokens.map(token => (
        <Grid key={token.name} item xs="auto">
          <ItemCard {...token}/>
        </Grid>
      ))}
    </Grid>
  );
}