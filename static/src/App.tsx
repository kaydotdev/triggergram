import { CssBaseline } from '@material-ui/core';
import {
  createTheme,
  ThemeProvider,
  makeStyles,
  createStyles,
  Theme
} from '@material-ui/core/styles';
import {
  Grid,
  Typography
} from '@material-ui/core';
import { indigo, blue } from '@material-ui/core/colors';
import NavBar from './components/NavBar';
import SearchBar from './components/SearchBar';
import MediaPost from './components/MediaPost';


const theme = createTheme({
  palette: {
    type: 'light',
    primary: indigo,
    secondary: blue
  }
});


const useStyles = makeStyles((_: Theme) =>
    createStyles({
        grid: {
            justifyContent: 'center',
            margin: '0 8px'
        },
        updates: {
            marginLeft: '24px'
        }
    }),
);


function App() {
  const classes = useStyles();

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline/>
      <NavBar />
      <SearchBar />
      <Typography gutterBottom variant="h6" className={classes.updates}>Recent updates</Typography>
      <Grid container className={classes.grid}>
        <MediaPost />
        <MediaPost />
        <MediaPost />
        <MediaPost />
        <MediaPost />
        <MediaPost />
      </Grid>
    </ThemeProvider>
  );
}

export default App;
