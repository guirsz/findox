\c findoxdb;

CREATE OR REPLACE FUNCTION public.fn_groups_get_count(
	in_group_id integer[])
    RETURNS integer
    LANGUAGE plpgsql
AS $BODY$
BEGIN
	RETURN (
		SELECT COUNT(*)
		FROM groups
		WHERE deleted = FALSE
		AND group_id = ANY(in_group_id)
	);
END
$BODY$;

ALTER FUNCTION public.fn_groups_get_count(integer[])
    OWNER TO postgres;