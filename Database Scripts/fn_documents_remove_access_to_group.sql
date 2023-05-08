CREATE OR REPLACE FUNCTION public.fn_documents_remove_access_to_group(
	in_document_id  	uuid,
	in_group_id  		integer)
    RETURNS void
    LANGUAGE SQL
AS $BODY$

	DELETE FROM public.grant_access_groups 
	WHERE 
		document_id = in_document_id
		AND group_id = in_group_id;

$BODY$;

ALTER FUNCTION public.fn_documents_remove_access_to_group(uuid, integer)
    OWNER TO postgres;
