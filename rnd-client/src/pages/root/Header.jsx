import {Box, Button, Typography} from "@mui/material";
import {FilterList} from "@mui/icons-material";

export default function Header () {
  return (
    <Box height="70px" width="100%" display="flex" justifyContent="space-between" alignContent="center">
      <Typography variant="h1">
        Page title
      </Typography>
      <Button startIcon={<FilterList/>} color="neutral">
        Последняя активность
      </Button>
    </Box>
  );
}