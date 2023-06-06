import {Box, Button, Typography} from "@mui/material";
import {FilterList} from "../../components/Icons";

export default function Header () {
  return (
    <Box height={70} width={1} display="flex" justifyContent="space-between" alignContent="center">
      <Typography variant="h1">
        Page title
      </Typography>
      <Button startIcon={<FilterList weight={400}/>} color="neutral">
        Последняя активность
      </Button>
    </Box>
  );
}