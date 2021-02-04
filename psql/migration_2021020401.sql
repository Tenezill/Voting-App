CREATE TABLE IF NOT EXISTS public."VoteResults"
(
    cats numeric NOT NULL,
    dogs numeric NOT NULL
);

ALTER TABLE public."VoteResults"
    OWNER to postgres;