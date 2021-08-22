import {
    Box,
    Typography,
    Paper
} from '@material-ui/core';
import {
    createStyles,
    makeStyles,
    Theme
} from '@material-ui/core/styles';
import { Skeleton } from '@material-ui/lab';

import { decimalMetric } from '../services/numerical';
import TimeAgo from 'timeago-react';


const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        mediaPost: {
            padding: 5,
            flexGrow: 3
        },
        mediaDescription: {
            padding: 10
        },
        photoWrapper: {
            position: 'relative',
            paddingBottom: '56.2%',
            marginBottom: '8px'
        },
        photo: {
            borderRadius: 5,
            position: 'absolute',
            objectFit: 'cover',
            width: '100%',
            height: '100%'
        }
    }),
);

interface MediaPostProps {
    src?: string,
    title?: string,
    account?: string,
    views?: number,
    createdAt?: Date
}

function MediaPost(props: MediaPostProps) {
    const classes = useStyles();

    return (
        <div className={classes.mediaPost}>
            <Paper>
                {
                props.src ? (
                    <div>
                        <div className={classes.photoWrapper}>
                            <img className={classes.photo} alt={props.title} src={props.src} />
                        </div>
                        <div className={classes.mediaDescription}>
                            <Box>
                                <Typography gutterBottom variant="body2">
                                    {props.title}
                                </Typography>
                                <Typography display="block" variant="caption" color="textSecondary">
                                    {props.account}
                                </Typography>
                                <Typography variant="caption" color="textSecondary">
                                    {decimalMetric(props.views || 0) + " views • "}
                                    <TimeAgo datetime={props.createdAt?.toUTCString() || new Date().toUTCString()} locale='en_us' />
                                </Typography>
                            </Box>
                        </div>
                    </div>
                ) : (
                    <div>
                        <Skeleton variant="rect" className={classes.photoWrapper} />
                        <div className={classes.mediaDescription}>
                            <Skeleton />
                            <Skeleton width="60%" />
                        </div>
                    </div>
                )
                }
            </Paper>
        </div>
    );
}

export default MediaPost;
