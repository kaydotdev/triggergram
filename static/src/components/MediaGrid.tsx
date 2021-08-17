import {
  makeStyles,
  createStyles,
  Theme
} from '@material-ui/core/styles';
import {
  Grid
} from '@material-ui/core';
import MediaPost from './MediaPost';


const useStyles = makeStyles((_: Theme) =>
    createStyles({
        grid: {
            justifyContent: 'center'
        },
        mediaContainer: {
          overflowY: 'auto'
        }
    }),
);


function MediaGrid() {
    const classes = useStyles();

    return (
      <div className={classes.mediaContainer}>
        <Grid container className={classes.grid}>
            <MediaPost />
            <MediaPost />
            <MediaPost />
            <MediaPost />
            <MediaPost />
            <MediaPost />
        </Grid>
      </div>
      );
}

export default MediaGrid;
