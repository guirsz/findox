CREATE OR REPLACE FUNCTION public.fn_documents_remove_access_to_user(
	in_document_id  	uuid,
	in_user_id  		integer)
    RETURNS void
    LANGUAGE SQL
AS $BODY$

	DELETE FROM public.grant_access_users 
	WHERE 
		document_id = in_document_id
		AND user_id = in_user_id;

$BODY$;

ALTER FUNCTION public.fn_documents_remove_access_to_user(uuid, integer)
    OWNER TO postgres;
