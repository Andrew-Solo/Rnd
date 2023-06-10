import {Box, Button, Typography} from "@mui/material";
import {FilterList} from "../../icons";
import {useEffect} from "react";

export default function DataHeader({title}) {
  useEffect(() => {
    document.title = `${title}`;
  })

  return (
    <Box height={70} width={1} display="flex" justifyContent="space-between" alignContent="center">
      <Typography variant="h1">
        {title}
      </Typography>
      <Button startIcon={<FilterList weight={400}/>} color="neutral" sx={{height: 40, px: 1.5, display: "none"}}>
        Последняя активность
      </Button>
    </Box>
  );
}